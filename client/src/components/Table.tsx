import Paper from '@material-ui/core/Paper';
import React, {useEffect, useState} from 'react';
import MaterialTable from 'material-table'
import MaterialUITable from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';

type ColumnData = {
   [key: string]: string;
};

type Props = {
   columnNames: string[];
   data: string[][];
};

export const Table: React.FC<Props> = (props: Props) => {
   const [data, setData] = useState<string[][]>([]);

   useEffect(() => {
      setData(props.data);
   }, [props]);

   return (
      <>
         {data && (
            <TableContainer component={Paper}>
               <MaterialUITable aria-label="customized table">
                  <TableHead>
                     <TableRow>
                        {props.columnNames.map((x, index) => {
                           if (index === 0) {
                              return <TableCell align="left">{x}</TableCell>;
                           }
                           return <TableCell align="right">{x}</TableCell>;
                        })}
                     </TableRow>
                  </TableHead>
                  <TableBody>
                     {data.map((x) => {
                        return (
                           <TableRow key="s">
                              {x.map((c, index) => {
                                 if (index === 0) {
                                    return (
                                       <TableCell component="th" scope="row">
                                          {c}
                                       </TableCell>
                                    );
                                 }
                                 return <TableCell align="right">{c}</TableCell>;
                              })}
                           </TableRow>
                        );
                     })}
                  </TableBody>
               </MaterialUITable>
            </TableContainer>
         )}
      </>
   );
};
