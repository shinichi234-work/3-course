n = int(input())
classes = list(map(int, input().split()))
m = int(input())

from collections import defaultdict

min_cost_for_power = defaultdict(lambda: float('inf'))

for _ in range(m):
    power, cost = map(int, input().split())
    min_cost_for_power[power] = min(min_cost_for_power[power], cost)

items = sorted(min_cost_for_power.items())

best = [float('inf')] * 1001
running_min = float('inf')

for power, cost in reversed(items):
    running_min = min(running_min, cost)
    for p in range(1, power + 1):
        best[p] = min(best[p], running_min)

total_cost = sum(best[req] for req in classes)
print(total_cost)