n, m = map(int, input().split())

max_val = None
max_row = 0
max_col = 0

for i in range(n):
    row = list(map(int, input().split()))
    for j in range(m):
        if max_val is None or row[j] > max_val:
            max_val = row[j]
            max_row = i
            max_col = j

print(max_row, max_col)