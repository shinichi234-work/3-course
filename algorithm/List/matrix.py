n, m = map(int, input().split())
matrix = []
for i in range(n):
    row = list(map(int, input().split()))
    matrix.append(row)

row_mins = []
for i in range(n):
    row_mins.append(min(matrix[i]))

col_maxs = []
for j in range(m):
    col_max = matrix[0][j]
    for i in range(1, n):
        if matrix[i][j] > col_max:
            col_max = matrix[i][j]
    col_maxs.append(col_max)

count = 0
for i in range(n):
    for j in range(m):
        if matrix[i][j] == row_mins[i] and matrix[i][j] == col_maxs[j]:
            count += 1

print(count)