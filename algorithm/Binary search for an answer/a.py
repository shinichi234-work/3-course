n = int(input())
k = int(input())

if 3 * k >= n:
    print(0)
else:
    left = 0
    right = n
    result = right
    
    while left <= right:
        mid = (left + right) // 2
        new_total = n + mid
        new_parents = k + mid
        
        if 3 * new_parents >= new_total:
            result = mid
            right = mid - 1
        else:
            left = mid + 1
    
    print(result)