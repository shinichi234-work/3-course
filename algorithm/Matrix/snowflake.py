n = int(input())

for i in range(n):
    row = []
    for j in range(n):
        if i == n // 2 or j == n // 2 or i == j or i + j == n - 1:
            row.append('*')
        else:
            row.append('.')
    print(' '.join(row))