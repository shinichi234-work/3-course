list1 = set(map(int, input().split()))
list2 = set(map(int, input().split()))
print(*sorted(list1 & list2))