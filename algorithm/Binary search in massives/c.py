n = int(input())

count = 0
power = 1

while power < n:
    power *= 2
    count += 1

print(count)