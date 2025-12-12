w, h, n = map(int, input().split())

left = 1
right = max(w, h) * n
result = right

while left <= right:
    mid = (left + right) // 2
    
    cols = mid // w
    rows = mid // h
    
    if cols * rows >= n:
        result = mid
        right = mid - 1
    else:
        left = mid + 1

print(result)