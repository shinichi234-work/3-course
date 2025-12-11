n = int(input())
arr = list(map(int, input().split()))

sorted_arr = sorted(arr)
min_global = min(arr)

position = {}
for i in range(n):
    position[arr[i]] = i

visited = [False] * n
total_cost = 0

for i in range(n):
    if visited[i] or arr[i] == sorted_arr[i]:
        continue
    
    cycle = []
    j = i
    while not visited[j]:
        visited[j] = True
        cycle.append(arr[j])
        j = position[sorted_arr[j]]
    
    if len(cycle) > 1:
        cycle_sum = sum(cycle)
        cycle_min = min(cycle)
        cycle_len = len(cycle)
        
        cost1 = cycle_sum + (cycle_len - 2) * cycle_min
        cost2 = cycle_sum + cycle_min + (cycle_len + 1) * min_global
        
        total_cost += min(cost1, cost2)

print(total_cost)