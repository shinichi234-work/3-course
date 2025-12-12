n = int(input())
shirts = list(map(int, input().split()))
m = int(input())
pants = list(map(int, input().split()))

i = 0
j = 0
min_diff = float('inf')
best_shirt = 0
best_pants = 0

while i < n and j < m:
    diff = abs(shirts[i] - pants[j])
    
    if diff < min_diff:
        min_diff = diff
        best_shirt = shirts[i]
        best_pants = pants[j]
    
    if shirts[i] < pants[j]:
        i += 1
    else:
        j += 1

print(best_shirt, best_pants)