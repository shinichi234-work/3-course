n1 = int(input())
arr1 = set(map(int, input().split()))

n2 = int(input())
arr2 = set(map(int, input().split()))

if arr1 == arr2:
    print("YES")
else:
    print("NO")