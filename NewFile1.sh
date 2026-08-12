git status
git add -A
git commit -m "WIP: save changes"   # or: git stash

git fetch origin
git checkout master
git pull origin master
git branch backup-master

# If you're currently on the feature branch, get its name:
BRANCH=$(git rev-parse --abbrev-ref HEAD)



git status --porcelain
# For all conflicted files, take 'theirs' (incoming branch) version:
git checkout --theirs -- .
git add -A
git commit -m "Resolve conflicts: prefer $BRANCH versions"

git push origin master