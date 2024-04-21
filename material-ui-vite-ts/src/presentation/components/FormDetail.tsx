import Typography from '@mui/material/Typography'
import { useGetPermission } from '../../application'
import TableContainer from '@mui/material/TableContainer'
import Table from '@mui/material/Table'
import TableHead from '@mui/material/TableHead'
import TableRow from '@mui/material/TableRow'
import TableCell from '@mui/material/TableCell'
import TableBody from '@mui/material/TableBody'
import Paper from '@mui/material/Paper'
import LinearProgress from '@mui/material/LinearProgress'
import Box from '@mui/material/Box'

interface FormDetailProps {}

export const FormDetail = ({}: FormDetailProps) => {
    const { data, isLoading, isError } = useGetPermission()

    return (
        <Box p={1} m={1} sx={{ width: '100%' }}>
            <Paper>
                <Box p={1} m={1} sx={{ width: '100%' }}>
                    <Typography variant="h5" component="h1" sx={{ mb: 2 }}>
                        Detail Permission
                    </Typography>

                    {isLoading && <LinearProgress />}
                    {isError && <span>Ups! it was an error</span>}

                    <TableContainer component={Paper}>
                        <Table sx={{ minWidth: 650 }} aria-label="simple table">
                            <TableHead>
                                <TableRow>
                                    <TableCell>Name</TableCell>
                                    <TableCell align="right">
                                        Last Name
                                    </TableCell>
                                    <TableCell align="right">
                                        Permission date
                                    </TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {data?.map(row => (
                                    <TableRow key={row.nombreEmpleado}>
                                        <TableCell component="th" scope="row">
                                            {row.nombreEmpleado}
                                        </TableCell>
                                        <TableCell align="right">
                                            {row.apellidoEmpleado}
                                        </TableCell>
                                        <TableCell align="right">
                                            {row.fechaPermiso?.toString()}
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Box>
            </Paper>
        </Box>
    )
}
