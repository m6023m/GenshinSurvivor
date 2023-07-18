import os
import subprocess


def exclude_files_from_commit():
    gitignore_path = '.gitignore'
    git_repo_path = 'C:/UniProject/GenshinSurvivor'  # Git 저장소의 경로

    with open(gitignore_path, 'r') as f:
        lines = f.readlines()
        excluded_files = [line.strip() for line in lines if line.strip() and not line.startswith('#')]

        for file in excluded_files:
            file_path = os.path.normpath(os.path.join(git_repo_path, file))

            # Remove the leading '\' from the file path
            if file_path.startswith('\\'):
                file_path = file_path[1:]

            print(f'file: {file_path}')
            if os.path.exists(file_path):
                subprocess.run(['git', '-C', git_repo_path, 'rm', '--cached', '-r', '--ignore-unmatch', file_path])
                print(f'Excluded file: {file_path}')

        subprocess.run(['git', '-C', git_repo_path, 'commit', '-m', 'Excluding files specified in .gitignore'])

        # Rewrite the commit history to remove the excluded files
        index_filter_command = ['git', '-C', git_repo_path, 'filter-branch', '--force', '--prune-empty', '--index-filter',
                                'git rm -rf --cached --ignore-unmatch -- .'] + excluded_files + ['--tag-name-filter', 'cat', '--', '--all']
        subprocess.run(index_filter_command)

        # Push the rewritten history to the remote repository
        subprocess.run(['git', '-C', git_repo_path, 'push', 'origin', '--force'])

        print('Commit history rewritten and pushed to remote repository.')


exclude_files_from_commit()
