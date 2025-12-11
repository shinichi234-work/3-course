n, m = map(int, input().split())
rows = []
for _ in range(n):
    row = list(map(int, input().split()))
    rows.append(row)

k = int(input())

result = 0
for i in range(n):
    for j in range(m - k + 1):
        found = True
        for idx in range(j, j + k):
            if rows[i][idx] != 0:
                found = False
                break
        if found:
            result = i + 1
            break
    if result != 0:
        break

print(result)