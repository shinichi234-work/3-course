n = int(input())

all_languages = None
any_languages = set()

for _ in range(n):
    m = int(input())
    student_languages = set()
    for _ in range(m):
        lang = input()
        student_languages.add(lang)
    
    if all_languages is None:
        all_languages = student_languages.copy()
    else:
        all_languages &= student_languages
    
    any_languages |= student_languages

if all_languages is None:
    all_languages = set()

print(len(all_languages))
for lang in sorted(all_languages):
    print(lang)

print(len(any_languages))
for lang in sorted(any_languages):
    print(lang)