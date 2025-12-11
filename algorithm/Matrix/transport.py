n, m = map(int, input().split())
A = []
for _ in range(n):
    row = list(map(int, input().split()))
    A.append(row)

for j in range(m):
    row = []
    for i in range(n):
        row.append(A[i][j])
    print(' '.join(map(str, row)))