import { ReactNode } from 'react'
import {
    useQuery,
    useMutation,
    useQueryClient,
    QueryClient,
    QueryClientProvider,
} from '@tanstack/react-query'
import ProTip from '../ProTip'
import Typography from '@mui/material/Typography'
import Link from '@mui/material/Link'
import Container from '@mui/material/Container'
import Box from '@mui/material/Box'

function Copyright() {
    return (
        <Typography variant="body2" color="text.secondary" align="center">
            {'Copyright © '}
            <Link color="inherit" href="https://mui.com/">
                App
            </Link>{' '}
            {new Date().getFullYear()}.
        </Typography>
    )
}

export const Home = () => {
    return (
        <Container maxWidth="sm">
            <Box sx={{ my: 4 }}>
                <Typography variant="h4" component="h1" sx={{ mb: 2 }}>
                    Hola mundo
                </Typography>
                <ProTip />
                <Copyright />
            </Box>
        </Container>
    )
}
