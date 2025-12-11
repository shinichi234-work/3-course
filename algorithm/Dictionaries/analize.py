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

word_list = []
for word, count in word_count.items():
    word_list.append((-count, word))

word_list.sort()

for count, word in word_list:
    print(word)