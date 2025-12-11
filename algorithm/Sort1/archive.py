s, n = map(int, input().split())
sizes = []
for _ in range(n):
    sizes.append(int(input()))

sizes.sort()

total = 0
count = 0
for size in sizes:
    if total + size <= s:
        total += size
        count += 1
    else:
        break

print(count)