n = int(input())
arr = list(map(int, input().split()))

max_index = 0
for i in range(n):
    if arr[i] > arr[max_index]:
        max_index = i

arr[0], arr[max_index] = arr[max_index], arr[0]

print(*arr)