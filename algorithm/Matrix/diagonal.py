n = int(input())

for i in range(n):
    row = []
    for j in range(n):
        row.append(abs(i - j))
    print(' '.join(map(str, row)))