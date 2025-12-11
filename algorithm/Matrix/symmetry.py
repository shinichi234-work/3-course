def IsSymmetric(A):
    n = len(A)
    for i in range(n):
        for j in range(n):
            if A[i][j] != A[j][i]:
                return False
    return True

n = int(input())
A = []
for _ in range(n):
    row = list(map(int, input().split()))
    A.append(row)

if IsSymmetric(A):
    print("YES")
else:
    print("NO")