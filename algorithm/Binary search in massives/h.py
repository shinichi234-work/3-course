n = int(input())
if n > 0:
    arr = list(map(int, input().split()))
else:
    arr = []
    input()
m = int(input())
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
    if n == 0:
        print(0)
    else:
        left_pos = find_left(arr, x, n)
        if left_pos == -1:
            print(0)
        else:
            right_pos = find_right(arr, x, n)
            print(right_pos - left_pos + 1)