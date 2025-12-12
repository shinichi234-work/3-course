n, k = map(int, input().split())
trees = list(map(int, input().split()))

from collections import defaultdict

count = defaultdict(int)
unique = 0
left = 0
min_len = n + 1
best_left = 0
best_right = 0

for right in range(n):
    if count[trees[right]] == 0:
        unique += 1
    count[trees[right]] += 1
    
    while unique == k:
        length = right - left + 1
        if length < min_len:
            min_len = length
            best_left = left + 1
            best_right = right + 1
        
        count[trees[left]] -= 1
        if count[trees[left]] == 0:
            unique -= 1
        left += 1

print(best_left, best_right)