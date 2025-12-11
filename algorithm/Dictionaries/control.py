n = int(input())
dictionary = {}

for _ in range(n):
    word = input().strip()
    word_lower = word.lower()
    
    if word_lower not in dictionary:
        dictionary[word_lower] = set()
    dictionary[word_lower].add(word)

text = input().split()
errors = 0

for word in text:
    word_lower = word.lower()
    uppercase_count = sum(1 for c in word if c.isupper())
    
    if word_lower in dictionary:
        if word not in dictionary[word_lower]:
            errors += 1
    else:
        if uppercase_count != 1:
            errors += 1

print(errors)