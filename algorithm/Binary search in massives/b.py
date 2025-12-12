n, k = map(int, input().split())
arr = list(map(int, input().split()))
queries = list(map(int, input().split()))

for x in queries:
    left, right = 0, n - 1
    
    if x <= arr[0]:
        print(arr[0])
        continue
    if x >= arr[-1]:
        print(arr[-1])
        continue
    
    while right - left > 1:
        mid = (left + right) // 2
        if arr[mid] <= x:
            left = mid
        else:
            right = mid
    
    if abs(arr[left] - x) <= abs(arr[right] - x):
        print(arr[left])
    else:
        print(arr[right])