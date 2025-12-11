n = int(input())
files = {}

for _ in range(n):
    parts = input().split()
    filename = parts[0]
    permissions = set(parts[1:])
    files[filename] = permissions

m = int(input())

operation_map = {
    'read': 'R',
    'write': 'W',
    'execute': 'X'
}

for _ in range(m):
    operation, filename = input().split()
    required_permission = operation_map[operation]
    
    if filename in files and required_permission in files[filename]:
        print("OK")
    else:
        print("Access denied")