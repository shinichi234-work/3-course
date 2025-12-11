n = int(input())
accounts = {}

for _ in range(n):
    query = input().split()
    
    if query[0] == '1':
        name = query[1]
        amount = int(query[2])
        
        if name in accounts:
            accounts[name] += amount
        else:
            accounts[name] = amount
    
    elif query[0] == '2':
        name = query[1]
        
        if name in accounts:
            print(accounts[name])
        else:
            print("ERROR")