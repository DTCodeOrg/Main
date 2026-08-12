git fetch origin
git checkout master
git pull origin master
git branch backup-master

# Switch to master and merge preferring the branch changes
git checkout master
git pull origin master
git merge --no-ff -X theirs "$BRANCH" -m "Merge $BRANCH into master (prefer branch)"