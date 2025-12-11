def SwapColumns(A, i, j):
    for row in A:
        row[i], row[j] = row[j], row[i]

n, m = map(int, input().split())
A = []
for _ in range(n):
    row = list(map(int, input().split()))
    A.append(row)

i, j = map(int, input().split())
SwapColumns(A, i, j)

for row in A:
    print(' '.join(map(str, row)))