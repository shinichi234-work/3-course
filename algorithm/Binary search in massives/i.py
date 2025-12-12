n, m = map(int, input().split())
arr = list(map(int, input().split()))
queries = list(map(int, input().split()))

def find_left(arr, x, n):
    left, right = 0, n - 1
    result = -1
    while left <= right:
        mid = (left + right) // 2
        if arr[mid] == x:
            result = mid
            right = mid - 1
        elif arr[mid] < x:
            left = mid + 1
        else:
            right = mid - 1
    return result

def find_right(arr, x, n):
    left, right = 0, n - 1
    result = -1
    while left <= right:
        mid = (left + right) // 2
        if arr[mid] == x:
            result = mid
            left = mid + 1
        elif arr[mid] < x:
            left = mid + 1
        else:
            right = mid - 1
    return result

for x in queries:
    left_pos = find_left(arr, x, n)
    if left_pos == -1:
        print(0)
    else:
        right_pos = find_right(arr, x, n)
        print(left_pos + 1, right_pos + 1)