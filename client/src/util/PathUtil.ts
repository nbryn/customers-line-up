export type PathInfo = {
   primaryPath: string;
   primaryButtonText: string;
   secondaryPath?: string;
   secondaryButtonText?: string;
};

function getPathAndTextFromURL(path: string): PathInfo {
   const endOfPath = path.substring(path.lastIndexOf('/') + 1);
   const primaryPath = getPrimaryPath(endOfPath);
   const primaryButtonText = getPrimaryButtonText(endOfPath);

   const secondaryPath = getSecondaryPath(endOfPath);
   const secondaryButtonText = getSecondaryButtonText(endOfPath);

   return {
      primaryPath,
      primaryButtonText,
      secondaryPath,
      secondaryButtonText,
   };
}

function getPrimaryPath(path: string): string {
   const newPath = path === 'business' ? 'manage' : path + '/manage';

   return newPath;
}

function getPrimaryButtonText(path: string): string {
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

function getSecondaryPath(path: string): string {
   if (path === 'timeslots' || path === 'employees') return path + '/new';

   return '';
}

function getSecondaryButtonText(path: string): string {
   if (path === 'timeslots') return 'New Time Slot';
   if (path === 'employees') return 'New Employee';

   return '';
}

export default {
   getPathAndTextFromURL,
};
