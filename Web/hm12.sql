DROP TABLE IF EXISTS claims_import_lines;

CREATE TABLE claims_import_lines (
    id SERIAL PRIMARY KEY,
    source_file TEXT NOT NULL,
    line_no INT NOT NULL,
    raw_line TEXT NOT NULL,
    received_at TIMESTAMPTZ DEFAULT NOW(),
    note TEXT
);

INSERT INTO claims_import_lines (source_file, line_no, raw_line, note) VALUES
('clinic_A_2025_11.csv', 1, 'Claim#C1001; Patient: Ivan Petrov <ivan.petrov@example.com>; +7 (900) 123-45-67; Policy: POL-12345', 'claim row'),
('clinic_A_2025_11.csv', 2, 'Claim#C1002; Patient: Olga S; olga.s@mail.co; 8-900-1234567; Policy: POL67890', 'claim row'),
('clinic_A_2025_11.csv', 3, 'Claim#C1003; Patient: Oops <bad@-domain.com>; 09001234567; Policy: BAD@@POL', 'broken email/policy'),
('billing_feed.csv', 10, 'proc: CPT-99213; amount: "1,200.00" USD; provider: Clinic A', 'billing row'),
('billing_feed.csv', 11, 'code: ICD10-A41.9; charge: "2 500,00" EUR; note: urgent', 'billing row'),
('diagnosis_tags.csv', 1, 'tags: urgent, inpatient, cardio', 'tags row'),
('diagnosis_tags.csv', 2, 'tags: outpatient, , followup', 'tags with empty'),
('patients_dirty.csv', 5, '"Ivanov, Ivan","123 Med St, Bldg 2","claim: C2001","1,500.00"', 'dirty csv'),
('patients_dirty.csv', 6, '"Brown, Sarah","Ward 7, Room 12","notes: needs translator","2 000,00"', 'dirty csv'),
('ingest_claims_log.txt', 400, 'INFO: started claims ingest', 'log'),
('ingest_claims_log.txt', 401, 'warning: missing policy for claim C1003', 'log'),
('ingest_claims_log.txt', 402, 'error: failed to parse patients_dirty.csv line 6', 'log'),
('ingest_claims_log.txt', 403, 'Error: amount format invalid for claim C1002', 'log'),
('clinic_A_2025_11.csv', 20, 'Patient: bad@@example..com; phone: +7 900 ABC-45-67; Policy: POL-12-!!', 'trap-bad-email-phone-policy'),
('patients_dirty.csv', 7, '"O''Neil, Patrick","100 Main St, Suite 5","claim: C2002",""', 'dirty csv with apostrophe and empty amount');

-- Задание 1: Найти все строки с корректным email (обычным или в угловых скобках)
SELECT
    id,
    source_file,
    line_no,
    raw_line
FROM claims_import_lines
WHERE raw_line ~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

-- Задание 2: Найти строки, НЕ содержащие корректный email
SELECT
    id,
    source_file,
    line_no,
    raw_line
FROM claims_import_lines
WHERE raw_line !~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

-- Задание 3: Извлечь первый email из каждой строки
SELECT
    id,
    source_file,
    line_no,
    (regexp_match(raw_line, '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}'))[1] AS email
FROM claims_import_lines
WHERE raw_line ~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

-- Задание 4: Найти и извлечь номера полисов/claim-id
SELECT
    id,
    source_file,
    line_no,
    (regexp_match(raw_line, '(POL-?\d+|C\d+)', 'i'))[1] AS policy_or_claim_id
FROM claims_import_lines
WHERE raw_line ~* '(POL-?\d+|C\d+)';

-- Задание 5: Извлечь все процедурные коды (CPT/ICD)
SELECT
    id,
    source_file,
    line_no,
    regexp_matches(raw_line, '(CPT-\d+|ICD\d*-[A-Z0-9.]+)', 'g') AS procedure_code
FROM claims_import_lines
WHERE raw_line ~ '(CPT-\d+|ICD\d*-[A-Z0-9.]+)';

-- Задание 6: Нормализовать суммы (убрать разделители тысяч)
SELECT
    id,
    source_file,
    line_no,
    raw_line,
    regexp_replace(
        regexp_replace(raw_line, '([0-9])[, ]([0-9])', '\1\2', 'g'),
        '"',
        '',
        'g'
    ) AS normalized_amounts
FROM claims_import_lines
WHERE raw_line ~ '[0-9]{1,3}[, ][0-9]{3}';

-- Задание 7: Нормализовать телефонные номера (только цифры)
SELECT
    id,
    source_file,
    line_no,
    raw_line,
    regexp_replace(raw_line, '[^0-9]', '', 'g') AS phone_digits_only
FROM claims_import_lines
WHERE raw_line ~ '(\+?[0-9][\s\(\)-]*){7,}';

-- Задание 8: Разбить теги на массив (убрать пустые элементы)
SELECT
    id,
    source_file,
    line_no,
    regexp_split_to_array(
        regexp_replace(raw_line, 'tags:\s*', '', 'i'),
        ',\s*'
    ) AS tags_array
FROM claims_import_lines
WHERE raw_line ~* 'tags:';

-- Задание 9: Разбить CSV-строки на отдельные поля
SELECT
    id,
    source_file,
    line_no,
    regexp_split_to_table(raw_line, '","') AS csv_field
FROM claims_import_lines
WHERE source_file = 'patients_dirty.csv';

-- Задание 10: Найти логи с "error" и заменить на "ERROR" (регистронезависимо)
SELECT
    id,
    source_file,
    line_no,
    raw_line AS original_line,
    regexp_replace(raw_line, 'error', 'ERROR', 'gi') AS normalized_line
FROM claims_import_lines
WHERE source_file = 'ingest_claims_log.txt'
    AND raw_line ~* 'error';