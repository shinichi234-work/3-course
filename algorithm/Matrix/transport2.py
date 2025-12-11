def Transpose(A):
    n = len(A)
    for i in range(n):
        for j in range(i + 1, n):
            A[i][j], A[j][i] = A[j][i], A[i][j]

n = int(input())
A = []
for _ in range(n):
    row = list(map(int, input().split()))
    A.append(row)

Transpose(A)

for row in A:
    print(' '.join(map(str, row)))