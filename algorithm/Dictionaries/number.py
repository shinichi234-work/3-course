words = []
count = {}

try:
    while True:
        line = input()
        for word in line.split():
            if word in count:
                words.append(count[word])
                count[word] += 1
            else:
                words.append(0)
                count[word] = 1
except EOFError:
    pass

print(*words)