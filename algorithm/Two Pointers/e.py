n1 = int(input())
caps = sorted(map(int, input().split()))
n2 = int(input())
shirts = sorted(map(int, input().split()))
n3 = int(input())
pants = sorted(map(int, input().split()))
n4 = int(input())
boots = sorted(map(int, input().split()))

def find_closest(arr, val):
    left, right = 0, len(arr) - 1
    closest = arr[0]
    
    while left <= right:
        mid = (left + right) // 2
        if abs(arr[mid] - val) < abs(closest - val):
            closest = arr[mid]
        
        if arr[mid] < val:
            left = mid + 1
        elif arr[mid] > val:
            right = mid - 1
        else:
            return arr[mid]
    
    return closest

min_diff = float('inf')
best = None

for cap in caps:
    for shirt in shirts:
        pant = find_closest(pants, max(cap, shirt))
        boot = find_closest(boots, max(cap, shirt, pant))
        
        current = [cap, shirt, pant, boot]
        diff = max(current) - min(current)
        
        if diff < min_diff:
            min_diff = diff
            best = current

print(*best)