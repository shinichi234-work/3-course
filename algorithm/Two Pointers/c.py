n, k = map(int, input().split())
s = input()

char_count = {}
left = 0
max_len = 0
max_start = 0

for right in range(n):
    char = s[right]
    char_count[char] = char_count.get(char, 0) + 1
    
    while char_count[char] > k:
        char_count[s[left]] -= 1
        left += 1
    
    length = right - left + 1
    if length > max_len:
        max_len = length
        max_start = left

print(max_len, max_start + 1)