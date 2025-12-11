n = int(input())
arr = list(map(int, input().split()))
m = int(input())

forbidden = set()
for _ in range(m):
    a, b = map(int, input().split())
    forbidden.add(frozenset([a, b]))

for i in range(n - 1):
    for j in range(n - 1):
        if arr[j] > arr[j + 1]:
            if frozenset([arr[j], arr[j + 1]]) not in forbidden:
                arr[j], arr[j + 1] = arr[j + 1], arr[j]

print(*arr)