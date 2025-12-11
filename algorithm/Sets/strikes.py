n, k = map(int, input().split())

strike_days = set()

for _ in range(k):
    a, b = map(int, input().split())
    day = a
    while day <= n:
        strike_days.add(day)
        day += b

count = 0
for day in strike_days:
    day_of_week = (day - 1) % 7 + 1
    if day_of_week != 6 and day_of_week != 7:
        count += 1

print(count)