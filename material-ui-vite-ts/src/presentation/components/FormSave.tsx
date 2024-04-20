import TextField from '@mui/material/TextField'
import { usePermissionService } from '../../application'
import { Permission } from '../../domain'
import Typography from '@mui/material/Typography'
import Button from '@mui/material/Button'
import { useForm, SubmitHandler } from 'react-hook-form'
import Paper from '@mui/material/Paper'
import Box from '@mui/material/Box'
import Grid from '@mui/material/Grid'
import LinearProgress from '@mui/material/LinearProgress'

interface FormSaveProps {}

export const FormSave = ({}: FormSaveProps) => {
    const create_permission = usePermissionService().useCreatePermission()

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm<Permission>()

    const onSubmit: SubmitHandler<Permission> = async (data: Permission) => {
        await create_permission.mutateAsync(data)
        reset()
    }

    return (
        <Box p={1} m={1} sx={{ width: '100%' }}>
            <Paper>
                <Box p={1} m={1} sx={{ width: '100%' }}>
                    <Typography variant="h5" component="h1" sx={{ mb: 2 }}>
                        Create Permission
                    </Typography>

                    <form onSubmit={handleSubmit(onSubmit)}>
                        <Grid
                            container
                            direction={'row'}
                            rowSpacing={1}
                            columnSpacing={1}
                        >
                            <Grid item>
                                <TextField
                                    label="Name"
                                    variant="outlined"
                                    size="small"
                                    error={errors.nombreEmpleado ? true : false}
                                    helperText={
                                        errors.nombreEmpleado
                                            ? 'Is required'
                                            : ''
                                    }
                                    {...register('nombreEmpleado', {
                                        required: true,
                                        maxLength: 50,
                                    })}
                                />
                            </Grid>
                            <Grid item>
                                <TextField
                                    label="Last Name"
                                    variant="outlined"
                                    size="small"
                                    error={
                                        errors.apellidoEmpleado ? true : false
                                    } // Set this based on your validation logic
                                    helperText={
                                        errors.apellidoEmpleado
                                            ? 'Is required'
                                            : ''
                                    }
                                    {...register('apellidoEmpleado', {
                                        required: true,
                                        maxLength: 50,
                                    })}
                                />
                            </Grid>
                            <Grid item>
                                <TextField
                                    label="Permission Type"
                                    variant="outlined"
                                    size="small"
                                    error={errors.tipoPermiso ? true : false} // Set this based on your validation logic
                                    helperText={
                                        errors.tipoPermiso ? 'Is required' : ''
                                    }
                                    {...register('tipoPermiso', {
                                        required: true,
                                        maxLength: 50,
                                    })}
                                />
                            </Grid>
                            <Grid item>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                >
                                    Submit
                                </Button>
                            </Grid>
                        </Grid>
                    </form>
                    {create_permission.isPending && (
                        <LinearProgress />
                    )}
                    {create_permission.isSuccess && (
                        <span>Created successfully ✅</span>
                    )}
                    {create_permission.isError && (
                        <span>Ups! it was an error 🚨</span>
                    )}
                </Box>
            </Paper>
        </Box>
    )
}
