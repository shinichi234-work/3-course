n, w = map(int, input().split())
l, r = map(int, input().split())

if n == 0:
    if r - l >= w:
        print(0)
    else:
        print(-1)
else:
    positions = list(map(int, input().split()))
    poles = [(positions[i], i + 1) for i in range(n)]
    poles.sort()
    
    min_remove = n + 1
    best_indices = []
    
    if poles[0][0] - l >= w:
        min_remove = 0
        best_indices = []
    
    if r - poles[-1][0] >= w and min_remove > 0:
        min_remove = 0
        best_indices = []
    
    if r - l >= w and n < min_remove:
        min_remove = n
        best_indices = [poles[k][1] for k in range(n)]
    
    right = 0
    for left in range(len(poles)):
        while right < len(poles) and poles[right][0] - poles[left][0] < w:
            right += 1
        
        if right < len(poles):
            remove_count = right - left - 1
            if remove_count < min_remove:
                min_remove = remove_count
                best_indices = [poles[k][1] for k in range(left + 1, right)]
    
    if min_remove == n + 1:
        print(-1)
    else:
        print(min_remove)
        for idx in best_indices:
            print(idx)