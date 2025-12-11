n = int(input())

for i in range(n):
    row = []
    for j in range(n):
        if i + j == n - 1:
            row.append(1)
        elif i + j < n - 1:
            row.append(0)
        else:
            row.append(2)
    print(' '.join(map(str, row)))