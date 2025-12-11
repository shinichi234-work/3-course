n = int(input())
arr = list(map(int, input().split()))

for i in range(1, n):
    key = arr[i]
    j = i - 1
    changed = False
    while j >= 0 and arr[j] > key:
        arr[j + 1] = arr[j]
        j -= 1
        changed = True
    arr[j + 1] = key
    if changed:
        print(*arr)