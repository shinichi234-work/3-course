word_count = {}

try:
    while True:
        line = input().split()
        for word in line:
            if word in word_count:
                word_count[word] += 1
            else:
                word_count[word] = 1
except EOFError:
    pass

max_count = max(word_count.values())
most_frequent = []

for word, count in word_count.items():
    if count == max_count:
        most_frequent.append(word)

print(sorted(most_frequent)[0])