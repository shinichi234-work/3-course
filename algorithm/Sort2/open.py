n, m = map(int, input().split())
matrix = []
for _ in range(n):
    row = list(map(int, input().split()))
    matrix.append(row)

flat = []
for i in range(n):
    for j in range(m):
        flat.append(matrix[i][j])

flat.sort()

position = {}
for i in range(n):
    for j in range(m):
        position[matrix[i][j]] = (i, j)

swaps = []
idx = 0

for i in range(n):
    for j in range(m):
        expected = flat[idx]
        if matrix[i][j] != expected:
            ci, cj = position[expected]
            
            swaps.append((i + 1, j + 1, ci + 1, cj + 1))
            
            matrix[i][j], matrix[ci][cj] = matrix[ci][cj], matrix[i][j]
            position[matrix[i][j]] = (i, j)
            position[matrix[ci][cj]] = (ci, cj)
        
        idx += 1

print(len(swaps))
for swap in swaps:
    print(*swap)