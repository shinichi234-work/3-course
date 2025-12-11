def SwapDiagonals(A):
    n = len(A)
    for j in range(n):
        i1 = j
        i2 = n - 1 - j
        A[i1][j], A[i2][j] = A[i2][j], A[i1][j]

n = int(input())
A = []
for _ in range(n):
    row = list(map(int, input().split()))
    A.append(row)

SwapDiagonals(A)

for row in A:
    print(' '.join(map(str, row)))