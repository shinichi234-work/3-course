foot_size = int(input())
sizes = list(map(int, input().split()))

sizes.sort()

count = 0
current_size = foot_size
for size in sizes:
    if size >= current_size:
        count += 1
        current_size = size + 3

print(count)