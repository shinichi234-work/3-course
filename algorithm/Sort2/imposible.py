n = int(input())
numbers = []
for _ in range(n):
    numbers.append(int(input()))

numbers.sort()

max_sum = 0
for num in numbers:
    if num > max_sum + 1:
        break
    max_sum += num

print(max_sum + 1)