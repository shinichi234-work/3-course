n, k = map(int, input().split())
arr = list(map(int, input().split()))
queries = list(map(int, input().split()))

def binary_search(arr, target):
    left = 0
    right = len(arr) - 1
    
    while left <= right:
        mid = (left + right) // 2
        
        if arr[mid] == target:
            return True
        elif arr[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    
    return False

for query in queries:
    if binary_search(arr, query):
        print("YES")
    else:
        print("NO")