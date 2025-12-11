x = int(input())
n = int(input())
table = []
for i in range(n):
    row = list(map(int, input().split()))
    table.append(row)

for col in range(n):
    found = False
    for row in range(n):
        if table[row][col] == x:
            found = True
            break
    if found:
        print('YES')
    else:
        print('NO')