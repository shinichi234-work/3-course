word = input()
n = int(input())
dictionary = []
for _ in range(n):
    dictionary.append(input())

left = 0
right = n - 1
found = False

while left <= right:
    mid = (left + right) // 2
    if dictionary[mid] == word:
        found = True
        break
    elif dictionary[mid] < word:
        left = mid + 1
    else:
        right = mid - 1

if found:
    print("YES")
else:
    print("NO")