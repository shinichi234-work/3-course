n, m = map(int, input().split())

A = []
for i in range(n):
    row = []
    for j in range(m):
        if i == 0 or j == 0:
            row.append(1)
        else:
            row.append(A[i-1][j] + row[j-1])
    A.append(row)

for row in A:
    for num in row:
        print(f'{num:6}', end='')
    print()