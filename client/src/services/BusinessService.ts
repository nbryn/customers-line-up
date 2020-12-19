export type PathInfo = {
    path: string;
    buttonText: string;
}

function getPathAndTextFromURL(path: string): PathInfo {
    const endOfPath = path.substring(path.lastIndexOf('/') + 1);

    const newPath = endOfPath === 'business' ? 'manage' : endOfPath + '/manage';

    const buttonText = getButtonText(endOfPath);

    return {
        path: newPath,
        buttonText
    }
}


function getButtonText(path: string): string {
    switch (path) {
        case 'business':
            return 'Edit Info';
        case 'bookings':
            return 'Manage Bookings';
        case 'timeslots':
            return 'Manage Time Slots';
        case 'employees':
            return 'Manage Employees';
        default:
            return '';
    }
}

export default {
    getPathAndTextFromURL,
}