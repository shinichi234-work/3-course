n = int(input())
A = []
for _ in range(n):
    row = list(map(int, input().split()))
    A.append(row)

k = int(input())

result = []
for i in range(n):
    j = i - k
    if 0 <= j < n:
        result.append(A[i][j])

print(' '.join(map(str, result)))