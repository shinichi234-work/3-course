words = set()
try:
    while True:
        line = input()
        for word in line.split():
            words.add(word)
except EOFError:
    pass

print(len(words))