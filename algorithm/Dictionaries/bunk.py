accounts = {}

try:
    while True:
        command = input().split()
        
        if command[0] == 'DEPOSIT':
            name = command[1]
            amount = int(command[2])
            if name not in accounts:
                accounts[name] = 0
            accounts[name] += amount
        
        elif command[0] == 'WITHDRAW':
            name = command[1]
            amount = int(command[2])
            if name not in accounts:
                accounts[name] = 0
            accounts[name] -= amount
        
        elif command[0] == 'BALANCE':
            name = command[1]
            if name in accounts:
                print(accounts[name])
            else:
                print("ERROR")
        
        elif command[0] == 'TRANSFER':
            name1 = command[1]
            name2 = command[2]
            amount = int(command[3])
            if name1 not in accounts:
                accounts[name1] = 0
            if name2 not in accounts:
                accounts[name2] = 0
            accounts[name1] -= amount
            accounts[name2] += amount
        
        elif command[0] == 'INCOME':
            percent = int(command[1])
            for name in accounts:
                if accounts[name] > 0:
                    accounts[name] += int(accounts[name] * percent / 100)

except EOFError:
    pass