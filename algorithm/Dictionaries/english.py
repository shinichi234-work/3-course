n = int(input())
latin_to_english = {}

for _ in range(n):
    line = input().strip()
    parts = line.split(' - ')
    english_word = parts[0]
    latin_words = parts[1].split(', ')
    
    for latin_word in latin_words:
        if latin_word not in latin_to_english:
            latin_to_english[latin_word] = []
        latin_to_english[latin_word].append(english_word)

print(len(latin_to_english))

for latin_word in sorted(latin_to_english.keys()):
    english_words = sorted(latin_to_english[latin_word])
    print(f"{latin_word} - {', '.join(english_words)}")