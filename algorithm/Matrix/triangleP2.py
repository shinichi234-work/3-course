n = int(input())

triangle = []
for i in range(n):
    row = []
    for j in range(i + 1):
        if j == 0 or j == i:
            row.append(1)
        else:
            row.append(triangle[i-1][j-1] + triangle[i-1][j])
    triangle.append(row)

for row in triangle:
    for num in row:
        print(str(num).rjust(6), end='')
    print()